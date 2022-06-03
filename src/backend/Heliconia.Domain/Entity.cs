using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        public bool IsRemoved { get; private set; }

        protected Entity(Guid? id = null)
        {
            Id = id != null ? id.Value : Guid.NewGuid();
            IsRemoved = false;
        }

        public void GoToDeletedState() => IsRemoved = true;

        public void GoToRecoveredState() => IsRemoved = false;

        public void GenerateNewId()
        {
            Id = Guid.NewGuid();
        }
    }
}
