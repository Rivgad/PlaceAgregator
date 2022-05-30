const axios = require('axios').default;

const login = (login, password) => {
    return axios
    .post("/api/Auth/Login", { login, password })
    .then((request) => {
      if (request.data.accessToken) {
        localStorage.setItem("user", JSON.stringify(request.data));
      }
      return request.data;
    })
    .catch((error) => { throw Error(error) } )
};

const registration = (login, password, passwordConfirm) => {
    return axios
    .post("/api/Auth/Registration", { login, password })
    .then((request) => {
      return request.data;
    })
    .catch((error) => { 
        console.log(error);
        throw Error(error);
    })
};

const logout = () => {
  localStorage.removeItem("user");
};

const authService = {
  login,
  registration,
  logout,
};

export default authService;