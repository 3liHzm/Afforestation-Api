using Afforestation.Domain.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afforestation.App.Posts
{
    [Service]
    public class DeletePost
    {
        private readonly IPostManager _postManager;

        public DeletePost(IPostManager postManager)
        {
            _postManager = postManager;
        }

        public async Task<bool> Do(int id)
        {
            if(await _postManager.DeletePost(id) > 0)
            {
                return true;
            }
            return false;
            
        }
    }
}
