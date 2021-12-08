import axios from 'axios'

export default {
    list() {
        return axios.get('/posts')
    },
    get(id) {
        return axios.get(`posts/${id}`)
    },
    create() {
        return axios.post('/posts')
    },
    update(postId){
        return axios.put(`/posts/${postId}`)
    },
    delete(postId) {
        return axios.delete(`/posts/${postId}`)
    }
}