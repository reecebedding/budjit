using budjit.core.data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using budjit.core.models;
using System.Linq;

namespace budjit.core.data.SQLite
{
    public class TagRepository : ITagRepository
    {
        private BudjitContext db;
        public TagRepository(BudjitContext context)
        {
            this.db = context;
        }
        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        public Tag GetById(int ID)
        {
            return db.Tags.Where(x => x.ID == ID).FirstOrDefault();
        }

        public Tag GetByName(string name)
        {
            return db.Tags.Where(x => x.Name == name).FirstOrDefault();
        }

        public Tag Create(Tag tag)
        {
            tag = db.Tags.Add(tag).Entity;
            db.SaveChanges();
            return tag;
        }

        public Tag Update(Tag tag)
        {
            tag = db.Tags.Update(tag).Entity;
            db.SaveChanges();
            return tag;
        }
    }
}
