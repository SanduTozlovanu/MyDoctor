import axios from './index'

class DoctorsApi {
  static async GetDoctors() {
    return await axios.get('/Doctor')
  }

}

export default DoctorsApi