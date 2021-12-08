using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IPostDao
    {
        Post GetPost(int postId);

        Post UploadPost(Post post);

        bool UpdatePost(Post post, int accountId);
    }
}
