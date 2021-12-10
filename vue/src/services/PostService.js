import axios from 'axios'

export default {
    list() {
        return axios.get('/posts')
    },
    get(id) {
        return axios.get(`posts/${id}`)
    },
    create(post) {
        return axios.post('/posts', post)
    },
    update(postId, post){
        return axios.put(`/posts/${postId}`, post)
    },
    delete(postId) {
        return axios.delete(`/posts/${postId}`)
    },
    likePost(_postId, _accountId) {
        postBody = {
            postId: _postId,
            accountId: _accountId
        }
        return axios.post(`/posts/${_postId}/like`, postBody)
    },
    unlikePost(_postId, _accountId) {
        // will need to use auth user context server side to determine what to delete
        return axios.delete(`/posts/${_postId}/like`)
    },
    getAllLikes(_postId) {
        return axios.get(`/posts/${_postId}/like`)
    },
    getFavorites(_accountId) {
        return axios.get(`/posts/favorites`, { params : { accountId: _accountId}});
    },
    addFavorite(_postId, _accountId) {
        postBody = {
            postId: _postId,
            accountId: _accountId
        }
        return axios.post("/posts/favorites", postBody);
    },
    removeFavorite(_postId) {
        // will need to use auth user context server side to determine what to delete
        return axios.delete(`/posts/favorites/${_postId}`)
    }
}