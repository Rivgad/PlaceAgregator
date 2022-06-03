import authHeader from './authHeader';

const axios = require('axios').default;

const login = (login, password) => {
    return axios
    .post("/api/Auth/Login", { login: login, password })
    .then((request) => {
      if (request.data.accessToken) {
        localStorage.setItem("user", JSON.stringify(request.data));
      }
      return request.data;
    })
    .catch((error) => { throw error } )
};

const registration = (email, userName, password, confirmPassword) => {
    return axios
    .post("/api/Auth/Registration", { email, userName, password, confirmPassword })
    .then((request) => {
      return request.data;
    })
    .catch((error) => { 
        console.log(error);
        throw Error(error);
    })
};

const getUserInfo = async () => {
  return await axios.get('/api/Profile',
    {
      headers: authHeader()
    });
}

const updateUserInfo = async (userName, email, firstName, lastName, patronimyc) => {
  return await axios.post('/api/Profile',
    {userName, email, firstName, lastName, patronimyc},
    {
      headers: authHeader()
    });
}

const logout = () => {
  localStorage.removeItem("user");
};

const authService = {
  login,
  registration,
  logout,
  getUserInfo,
  updateUserInfo
};

export default authService;