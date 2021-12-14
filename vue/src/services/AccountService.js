import axios from 'axios';

//Do I make a http variable or just use 'axios'?

export default {
    
    getAccount(accountId) {
        return axios.get(`/accounts/${accountId}`);
    },
    getAccountDetails(accountId) {
        return axios.get(`accounts/${accountId}/details`);
    }
}