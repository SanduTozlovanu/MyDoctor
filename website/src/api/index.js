import Axios from 'axios'

const axios = Axios.create({
  baseURL: 'https://localhost:7244/api',
  headers: { 'Content-Type': 'application/json' },
})

axios.interceptors.request.use(
  (config) => {
    const token = 'faketoken'
    config.headers.Authorization = token
    return Promise.resolve(config)
  },
  (error) => Promise.reject(error),
)

axios.interceptors.response.use(
  (response) => Promise.resolve(response),
  (error) => Promise.reject(error),
)

export default axios
