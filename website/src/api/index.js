import Axios from 'axios'

const axios = Axios.create({
  baseURL: 'https://localhost:7244/api',
  headers: { 'Content-Type': 'application/json' },
})

axios.interceptors.request.use(
  (config) => {
    let user = localStorage.getItem('user')
    if (user) {
      user = JSON.parse(user)
      config.headers.Authorization = user && user.jwtToken ? user.jwtToken : null
    }
    return Promise.resolve(config)
  },
  (error) => Promise.reject(error),
)

axios.interceptors.response.use(
  (response) => Promise.resolve(response),
  (error) => Promise.reject(error),
)

export default axios
