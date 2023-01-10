import axios from './index'

class DrugApi {
  static async GetDrugs() {
    return await axios.get('/Drugs')
  }
  static async GetDrugsByDoctorId(doctorId) {
    return await axios.get(`/Drugs/${doctorId}`)
  }
}

export default DrugApi