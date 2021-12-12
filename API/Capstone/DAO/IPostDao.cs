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

        List<Post> GetListOfPostsByAccountId(int accountId);

        List<Post> GetAllPosts();

        Post UploadPost(Post post);

        bool UpdatePost(int postId, string mediaLink);
    }
}
