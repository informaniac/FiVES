
using System;
using System.Collections.Generic;

namespace FIVES
{
    /// <summary>
    /// Manages entities in the database
    /// </summary>
    public class EntityRegistry
    {
        public readonly static EntityRegistry Instance = new EntityRegistry();

        /// <summary>
        /// Adds a new entity without any Guid assigned yet to the database.
        ///  A new Guid is generated for the entity and returned by the function
        /// </summary>
        /// <returns>Guid assigned to the new Entity</returns>
        /// <param name="entity">Entity that is to be added to the registry</param>
        public Guid addEntity(Entity entity)
        {
            Guid newGUID = Guid.NewGuid();
            entity.Guid = newGUID;
            this.addEntityWithGUID(entity);
            return newGUID;
        }

        /// <summary>
        /// Adds an entity that has already a Guid assigned to the registry.
        /// </summary>
        /// <param name="entity">Entity to be added to the registry</param>
        public void addEntityWithGUID(Entity entity)
        {
            entities[entity.Guid] = entity;
        }

        /// <summary>
        ///  Removes an entity with a given <b>guid</b>. Returns false if such entity was not found.
        /// </summary>
        /// <returns><c>true</c>, if entity was removed, <c>false</c> otherwise.</returns>
        /// <param name="guid">GUID of the entity that should be removed</param>
        public bool removeEntity(Guid guid)
        {
            if (entities.ContainsKey(guid)) {
                entities.Remove(guid);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns entity by its <b>guid</b> or null if no such entity is found.
        /// </summary>
        /// <returns>The entity by GUID.</returns>
        /// <param name="guid">GUID.</param>
        public Entity getEntityByGuid(Guid guid)
        {
            if (entities.ContainsKey(guid))
                return entities[guid];
            return null;
        }

        /// <summary>
        ///  Returns a list of all entities' GUIDs.
        /// </summary>
        /// <returns>The all GUI ds.</returns>
        public List<Guid> getAllGUIDs()
        {
            List<Guid> res = new List<Guid>();
            foreach (Guid guid in entities.Keys)
                res.Add(guid);
            return res;
        }

        // Users should not construct EntityRegistry on their own, but use EntityRegistry.Instance instead.
        internal EntityRegistry() {}

        private IDictionary<Guid, Entity> entities = new Dictionary<Guid, Entity>();
        public readonly Guid RegistryGuid = new Guid("0f5f96c5-30cb-4b4f-b06f-e5efd257a3c9");
    }
}

