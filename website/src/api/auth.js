import axios from './index'

class AuthApi {
  static async RegisterPatient(data) {
    return await axios.post('/Patients', data)
  }

  static async RegisterDoctor(data) {
    return await axios.post('/Doctor', data)
  }

  static async Login(data) {
    return await axios.post('/Login', data)
  }
}

export default AuthApi
