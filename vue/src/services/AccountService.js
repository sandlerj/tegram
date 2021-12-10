import axios from 'axios';

//Do I make a http variable or just use 'axios'?

export default {
    favorites(user) {
        return axios.get(`/favorites/${user}`);
    },
    viewPosts(accountId) {
        return axios.get(`/account/${user}`);
    },
    

}