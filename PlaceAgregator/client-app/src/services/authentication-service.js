const axios = require('axios').default;


export default class AuthService {
    constructor() {
        this.currentAccount = null;
    }
    async Login(login, password) {
        let result = null;

            await axios.post(`/api/auth/login?login=${login}&password=${password}`, {
                validateStatus: (status)=>{
                    return status===200;
                }
            })
            .then((response)=>{
                result = response.data;
                localStorage.setItem('token', result.access_token);
            })
                .catch(
                    (error)=>{throw new Error(error);}
                )
                .finally(
                    ()=>{
                        return result;
                    }
                );
    }
    Logout() {
        this.currentAccount = null;
    }
    async Registration(login, password){
        try {
            await axios.post(`/api/auth/registration?login=${login}&password=${password}`)
            .then(request=>{
                if(request.status !== 200){
                    throw new Error("huinya");
                }
            })
            .catch(error=>{
                throw new Error(error.message)
            });
        } catch (error) {
            throw new Error(error);
        }
    }
    IsAuthenticated = () => this.currentAccount == null ? false : true;

}

