using budjit.core.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace budjit.core.data.Contracts
{
    public interface ITagRepository
    {
        Tag GetTagById(int ID);
        IEnumerable<Tag> GetAllTags();
    }
}
