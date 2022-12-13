import axios from './index'

class DoctorApi {
  static async GetDoctors() {
    return await axios.get('/Doctor')
  }

  static async UpdateDoctor(data) {
    return await axios.put(`/Doctor/${data.userId}`, data)
  }
  static async DeleteDoctor(userId){
    return await axios.delete(`/Doctor/${userId}`)
  }
}

export default DoctorApi