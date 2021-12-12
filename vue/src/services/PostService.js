import axios from 'axios'

export default {
    list(limit=20) {
        return axios.get('/posts', {params:{limit}}) // limit param not used serverside
    },
    get(id) {
        return axios.get(`posts/${id}`)
    },
    create(post) { // see notes from tom/change content 
        return axios.post('/posts', post)
    },
    update(postId, post){
        return axios.put(`/posts/${postId}`, post)
    },
    delete(postId) {
        return axios.delete(`/posts/${postId}`)
    },
    likePost(_postId, _accountId) {
        let postBody = {
            postId: _postId,
            accountId: _accountId
        }
        return axios.post(`/posts/${_postId}/like`, postBody)
    },
    unlikePost(_postId, _accountId) {
        // will need to use auth user context server side to determine what to delete
        return axios.delete(`/posts/${_postId}/like`, {data: {postId: _postId, accountId: _accountId}})
    },
    getAllLikes(_postId) {
        return axios.get(`/posts/${_postId}/like`)
    },
    getFavorites(_accountId) {
        return axios.get(`/posts/favorites/${_accountId}`);
    },
    addFavorite(_postId, _accountId) {
        let postBody = {
            postId: _postId,
            accountId: _accountId
        }
        return axios.post("/posts/favorites", postBody);
    },
    removeFavorite(_postId, _accountId) {
        // will need to use auth user context server side to determine what to delete
        return axios.delete(`/posts/favorites/${_postId}`, {data: {postId: _postId, accountId: _accountId}})
    }
}