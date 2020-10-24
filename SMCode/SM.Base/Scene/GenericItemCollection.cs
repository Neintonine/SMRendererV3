#region usings

using System.Collections.Generic;
using SM.Base.Contexts;

#endregion

namespace SM.Base.Scene
{
    /// <summary>
    ///     Contains a list of show items.
    /// </summary>
    /// <typeparam name="TItem">The type of show items.</typeparam>
    public abstract class GenericItemCollection<TItem> : List<TItem>, IShowItem, IShowCollection<TItem>
        where TItem : IShowItem
    {
        /// <inheritdoc />
        public List<TItem> Objects => this;

        /// <inheritdoc />
        public object Parent { get; set; }

        /// <inheritdoc />
        public string Name { get; set; } = "Unnamed Item Collection";

        /// <inheritdoc />
        public ICollection<string> Flags { get; set; } = new[] {"collection"};

        /// <inheritdoc />
        public virtual void Update(UpdateContext context)
        {
            for (var i = 0; i < Objects.Count; i++)
                this[i].Update(context);
        }

        /// <inheritdoc cref="IShowCollection{TItem}.Draw" />
        public virtual void Draw(DrawContext context)
        {
            for (var i = 0; i < Objects.Count; i++)
                this[i].Draw(context);
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
        ///     Adds a item.
        /// </summary>
        public new void Add(TItem item)
        {
            base.Add(item);
            item.Parent = this;
            item.OnAdded(this);
        }

        /// <summary>
        ///     Removes a item.
        /// </summary>
        /// <param name="item"></param>
        public new void Remove(TItem item)
        {
            base.Remove(item);
            item.Parent = null;
            item.OnRemoved(this);
        }

        /// <summary>
        ///     Returns a object with this name or the default, if not available.
        ///     <para>Not reclusive.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TItem GetItemByName(string name)
        {
            TItem obj = default;
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
            where TGetItem : TItem
        {
            return (TGetItem) GetItemByName(name);
        }

        /// <summary>
        ///     Returns all object that have this flag.
        ///     <para>Only in this list.</para>
        /// </summary>
        public ICollection<TItem> GetItemsWithFlag(string flag)
        {
            var list = new List<TItem>();
            for (var i = 0; i < Count; i++)
            {
                var obj = this[i];
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
    public abstract class GenericItemCollection<TItem, TTransformation> : GenericItemCollection<TItem>
        where TItem : IShowItem
        where TTransformation : GenericTransformation, new()
    {
        /// <summary>
        ///     Transformation of the collection
        /// </summary>
        public TTransformation Transform = new TTransformation();

        /// <inheritdoc />
        public override void Draw(DrawContext context)
        {
            context.ModelMaster = Transform.GetMatrix() * context.ModelMaster;

            base.Draw(context);
        }
    }
}