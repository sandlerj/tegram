import axios from 'axios';

export default {
    createComment(_postId, comment) {
        return axios.post(`/posts/${_postId}/comments`, comment);
    },
    getComments(_postId) {
        return axios.get(`/posts/${_postId}/comments`);
    },
    deleteComment(_postId, _commentId) {
        return axios.delete(`/posts/${_postId}/comments/${_commentId}`);
    }
}
