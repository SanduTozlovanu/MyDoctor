import axios from './index'

class DrugApi {
  static async GetDrugs() {
    return await axios.get('/Drugs')
  }
}

export default DrugApi