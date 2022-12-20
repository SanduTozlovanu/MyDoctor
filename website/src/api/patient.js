import axios from './index'

class PatientApi {
  static async UpdatePatient(data) {
    return await axios.put(`/Patients/${data.userId}`, data)
  }
  static async DeletePatient(userId){
    return await axios.delete(`Patients/${userId}`)
  }
}

export default PatientApi