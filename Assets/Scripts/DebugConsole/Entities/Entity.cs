namespace DebugConsole.Entities
{
    public abstract class Entity
    {
        public int EntityID { get; }

  
        protected Entity(int entityID)
        {
            EntityID = entityID;
        }
    }
}
