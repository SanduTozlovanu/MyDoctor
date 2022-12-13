import axios from './index'

class PatientApi {
  static async UpdatePatient(data) {
    return await axios.put(`/Patient/${data.userId}`, data)
  }
  static async DeletePatient(userId){
    return await axios.delete(`Patient/${userId}`)
  }
}

export default PatientApi