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
    }
}
