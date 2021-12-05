import axios from 'axios';

//Do I make a http variable or just use 'axios'?

export default {
    favorites(user) {
        return axios.get(`/favorites/${user}`);
    },
    //Are we using userID or accountID to view post?
    viewPosts(user) {
        return axios.get(`/account/${user}`);
    },
    

}