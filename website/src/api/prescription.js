import axios from './index'

class PrescriptionApi {
  static async CreatePrescription(appointmentId, data) {
    return await axios.post(`/Prescriptions/${appointmentId}`, data)
  }
}

export default PrescriptionApi