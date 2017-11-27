using budjit.core.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.data.Contracts
{
    public interface ITagRepository
    {
        Tag GetById(int ID);
        Tag GetByName(string name);
        IEnumerable<Tag> GetAll();
        Tag Create(Tag tag);
        Tag Update(Tag tag);
    }
}
