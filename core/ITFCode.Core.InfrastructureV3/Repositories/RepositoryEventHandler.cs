using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITFCode.Core.InfrastructureV3.Repositories
{
    internal class RepositoryEventHandler
    {
        public event EventHandler<object> BeforeInsert;
        public event EventHandler<object> AfterInsert;
        
        public event EventHandler<object> BeforeInsertRange;
        public event EventHandler<object> AfterInsertRange;
        
        public event EventHandler<object> BeforeUpdate;
        public event EventHandler<object> AfterUpdate;
        
        public event EventHandler<object> BeforeUpdateRange;
        public event EventHandler<object> AfterUpdateRange;
        
        public event EventHandler<object> BeforeDelete;
        public event EventHandler<object> AfterDelete;
        
        public event EventHandler<object> BeforeDeleteRange;
        public event EventHandler<object> AfterDeleteRange;

        public event EventHandler<object> BeforeCommit;
        public event EventHandler<object> AfterCommit;

        protected virtual void OnBeforeInsert(object e) => BeforeInsert?.Invoke(this, e);
        protected virtual void OnAfterInsert(object e) => AfterInsert?.Invoke(this, e);

        protected virtual void OnBeforeInsertRange(object e) => BeforeInsertRange?.Invoke(this, e);
        protected virtual void OnAfterInsertRange(object e) => AfterInsertRange?.Invoke(this, e);

        protected virtual void OnBeforeUpdate(object e) => BeforeUpdate?.Invoke(this, e);
        protected virtual void OnAfterUpdate(object e) => AfterUpdate?.Invoke(this, e);

        protected virtual void OnBeforeUpdateRange(object e) => BeforeUpdateRange?.Invoke(this, e);
        protected virtual void OnAfterUpdateRange(object e) => AfterUpdateRange?.Invoke(this, e);

        protected virtual void OnBeforeDelete(object e) => BeforeDelete?.Invoke(this, e);
        protected virtual void OnAfterDelete(object e) => AfterDelete?.Invoke(this, e);

        protected virtual void OnBeforeDeleteRange(object e) => BeforeDeleteRange?.Invoke(this, e);
        protected virtual void OnAfterDeleteRange(object e) => AfterDeleteRange?.Invoke(this, e);
    }
}