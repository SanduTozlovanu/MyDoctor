import axios from './index'

class PatientApi {
  static async UpdatePatient(userId, data) {
    return await axios.put(`/Patients/${userId}`, data)
  }
  static async DeletePatient(userId){
    return await axios.delete(`Patients/${userId}`)
  }
}

export default PatientApi