import axios from 'axios';

export default {
    createComment(comment) {
        return axios.post(`/posts/${comment.postId}/comments`, comment);
    },
    getComments(_postId) {
        return axios.get(`/posts/${_postId}/comments`);
    }
}
