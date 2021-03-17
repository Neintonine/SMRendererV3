#region usings

using System.Collections.Generic;
using System.Collections.ObjectModel;
using SM.Base.Drawing;
using SM.Base.Windows;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Contains a list of show items.
    /// </summary>
    public abstract class GenericItemCollection : List<IShowItem>, IShowItem, IShowCollection, IScriptable, IFixedScriptable
    {
        private List<IScriptable> _scriptableObjects = new List<IScriptable>();

        /// <summary>
        ///     Currently active script objects.
        /// </summary>
        public ReadOnlyCollection<IScriptable> ScriptableObjects =>
            new ReadOnlyCollection<IScriptable>(_scriptableObjects);

        /// <inheritdoc />
        public bool UpdateActive { get; set; } = true;

        /// <inheritdoc />
        public List<IShowItem> Objects => this;

        /// <inheritdoc />
        public object Parent { get; set; }

        /// <inheritdoc />
        public string Name { get; set; } = "Unnamed Item Collection";

        /// <inheritdoc />
        public ICollection<string> Flags { get; set; } = new List<string> {"collection"};

        /// <inheritdoc cref="IShowItem" />
        public bool Active { get; set; } = true;

        /// <inheritdoc />
        public bool RenderActive { get; set; } = true;

        public virtual void FixedUpdate(FixedUpdateContext context)
        {
            if (!Active || !UpdateActive) return;

            for (int i = 0; i < _fixedScriptables.Count; i++)
            {
                _fixedScriptables[i].FixedUpdate(context);
            }
        }

        /// <inheritdoc cref="IShowCollection.Draw" />
        public virtual void Draw(DrawContext context)
        {
            if (!Active || !RenderActive) return;

            for (var i = 0; i < Objects.Count; i++)
            {
                if (!this[i].Active || !this[i].RenderActive) continue;
                this[i].Draw(context);
            }
        }

        /// <inheritdoc />
        public virtual void Update(UpdateContext context)
        {
            if (!Active || !UpdateActive) return;

            for (var i = 0; i < _scriptableObjects.Count; i++)
            {
                if (!_scriptableObjects[i].Active || !_scriptableObjects[i].UpdateActive) continue;
                _scriptableObjects[i].Update(context);
            }
        }

        /// <inheritdoc />
        public virtual void OnAdded(object sender)
        {
        }

        /// <inheritdoc />
        public virtual void OnRemoved(object sender)
        {
        }

        /// <summary>
        ///     Adds a item to the draw and the script collection, when applicable.
        /// </summary>
        public void Add(params IShowItem[] items)
        {
            foreach (var item in items)
            {
                AddObject(item);

                if (item is IScriptable scriptable)
                    AddScript(scriptable);

                if (item is IFixedScriptable fixedScriptable) _fixedScriptables.Add(fixedScriptable);
            }
        }

        /// <summary>
        ///     Adds the object to the collection.
        /// </summary>
        /// <param name="item"></param>
        public void AddObject(IShowItem item)
        {
            base.Add(item);
            item.Parent = this;
            item.OnAdded(this);
        }

        /// <summary>
        ///     Adds the script to the collection.
        /// </summary>
        /// <param name="item"></param>
        public void AddScript(IScriptable item)
        {
            _scriptableObjects.Add(item);
            if (item is IFixedScriptable fs) _fixedScriptables.Add(fs);
        }

        public void Remove(params IShowItem[] items)
        {
            foreach (var item in items)
            {
                RemoveObject(item);

                if (item is IScriptable scriptable)
                    RemoveScript(scriptable);

                if (item is IFixedScriptable fixedScriptable)
                    _fixedScriptables.Remove(fixedScriptable);
            }
        }

        /// <summary>
        ///     Remove the object from the draw collection.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveObject(IShowItem item)
        {
            base.Remove(item);
            item.Parent = null;
            item.OnRemoved(this);
        }

        /// <summary>
        ///     Remove the object from the script collection.
        /// </summary>
        /// <param name="item"></param>
        public void RemoveScript(IScriptable item)
        {
            _scriptableObjects.Remove(item);
            if (item is IFixedScriptable fs) _fixedScriptables.Remove(fs);
        }

        public ICollection<IShowItem> GetAllItems(bool includeCollections = false)
        {
            List<IShowItem> items = new List<IShowItem>();
            for (var i = 0; i < Count; i++)
            {
                if (!includeCollections && this[i] is IShowCollection) continue;
                items.Add(this[i]);
            }

            return items;
        }

        /// <summary>
        ///     Returns a object with this name or the default, if not available.
        ///     <para>Not reclusive.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IShowItem GetItemByName(string name)
        {
            IShowItem obj = default;
            for (var i = 0; i < Count; i++)
                if (this[i].Name == name)
                {
                    obj = this[i];
                    break;
                }

            return obj;
        }

        /// <summary>
        ///     Returns a object with this name or the default if not available.
        ///     <para>Not reclusive.</para>
        /// </summary>
        /// <typeparam name="TGetItem">Type of return</typeparam>
        public TGetItem GetItemByName<TGetItem>(string name)
            where TGetItem : IShowItem
        {
            return (TGetItem) GetItemByName(name);
        }

        /// <summary>
        ///     Returns all object that have this flag.
        ///     <para>Only in this list.</para>
        /// </summary>
        public ICollection<IShowItem> GetItemsWithFlag(string flag)
        {
            var list = new List<IShowItem>();
            for (var i = 0; i < Count; i++)
            {
                var obj = this[i];
                if (obj.Flags == null) continue;
                if (obj.Flags.Contains(flag)) list.Add(obj);
            }

            return list;
        }
    }

    /// <summary>
    ///     Contains a list of show items with transformation.
    /// </summary>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    /// <typeparam name="TTransformation">The type of transformation.</typeparam>
    public abstract class GenericItemCollection<TTransformation> : GenericItemCollection,
        IShowTransformItem<TTransformation>
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        ///     Transformation of the collection
        /// </summary>
        public TTransformation Transform { get; set; } = new TTransformation();

        /// <inheritdoc />
        public override void Draw(DrawContext context)
        {
            Transform.LastMaster = context.ModelMatrix;
            context.ModelMatrix = Transform.MergeMatrix(context.ModelMatrix);

            base.Draw(context);
        }
    }
}