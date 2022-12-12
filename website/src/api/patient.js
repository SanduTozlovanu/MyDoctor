import axios from './index'

class PatientApi {
  static async UpdatePatient(data) {
    return await axios.put(`/Patient/${data.userId}`, data)
  }

}

export default PatientApi