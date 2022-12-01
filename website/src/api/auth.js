import axios from './index'

class AuthApi {
  // static async Login(data){
  //     return await axios.post(`${}`)
  // }
  static async RegisterPatient(data) {
    return await axios.post('/Patients', data)
  }

  static async RegisterDoctor(data) {
    return await axios.post('/Doctor', data)
  }
}

export default AuthApi
