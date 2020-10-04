using System;

namespace Lunch.Domain.Entities
{
    public class Restaurant : EntityBase
    {
        public string Name { get; private set; }

        public Restaurant(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public override bool Equals( object obj )
        {
            if ( obj != null )
                if ( obj.GetType() == typeof( Restaurant ) )
                    if ( this.Id == ((Restaurant)obj).Id )
                        return true;

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( Id, Name );
        }
    }
}
