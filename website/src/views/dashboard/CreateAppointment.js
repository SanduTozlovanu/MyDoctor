/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from 'react'
// react component that copies the given text inside your clipboard
// reactstrap components
import {
  Card,
  CardHeader,
  CardBody,
  Container,
  Row,
  Col,
  // eslint-disable-next-line
  Label,
  Button,
  CardFooter,
} from 'reactstrap'
import 'react-modern-calendar-datepicker/lib/DatePicker.css'
import { Calendar } from 'react-modern-calendar-datepicker'
import Select from 'react-select'
import DoctorApi from 'api/doctor'
// core components
import Header from 'components/Headers/Header.js'

const CreateAppointment = () => {
  const [date, setDate] = useState(new Date())
  const [speciality, setSpeciality] = useState('')
  const [specialities, setSpecialities] = useState([])
  const [error, setError] = useState('')
  const [doctors, setDoctors] = useState([])
 
  useEffect(() => {
    const fetchData = async () => {
      await getSpecialities()
    }
    fetchData()
  }, [])

  const getSpecialities = async () => {
    try {
      const response = await DoctorApi.GetSpecialities()
      setSpecialities(response.data)
    } catch (err) {
      setError(err)
    }
  }

  const getDoctors = async () => {
    if (!speciality) {
      return
    }
    try {
      const response = await DoctorApi.GetDoctorsBySpeciality(speciality)
      console.log(response.data)
      setDoctors(response.data)
    } catch (err) {
      setError(err)
    }
  }

  useEffect(() => {
    const fetchData = async () => {
      await getDoctors()
    }
    fetchData()
  }, [speciality])

  const crypto = window.crypto

  return (
    <>
      <Header />
      <Container className="mt--7" fluid>
        {/* Table */}
        <Row>
          <Col>
            <Card className="shadow">
              <CardHeader className="border-0 pb-0">
                <h1 className="mb-0 text-center font-weight-700">
                  Make an Appointment
                </h1>
              </CardHeader>
              <CardBody>
                <Row>
                  <Col lg="6" md="12" sm="12" xs="12" className="text-left">
                    <h3>Select a speciality</h3>
                    <Select
                      onChange={(spec) => setSpeciality(spec ? spec.value : '')}
                      defaultValue={null}
                      isSearchable
                      isClearable
                      name="speciality"
                      options={specialities.map((item) => {
                        return { label: item.name, value: item.id }
                      })}
                      className="basic-single"
                      classNamePrefix="select"
                    />
                    <h3 className="mt-3">Choose your Doctor</h3>
                    <Row>
                      {doctors && doctors.length ? doctors.map((doc, index) => {
                        var array = new Uint32Array(1)
                        return (
                          <Col
                            className="text-left"
                            xl="12"
                            lg="12"
                            md="6"
                            sm="6"
                            xs="6"
                            key={index}
                          >
                            <Card className='shadow'>
                              <CardBody>
                                <Row className="align-items-center">
                                  <Col
                                    xl="2"
                                    lg="3"
                                    md="12"
                                    sm="12"
                                    xs="12"
                                    className="text-center text-lg-left"
                                  >
                                    <img
                                      className="rounded-circle avatar-lg"
                                      src={
                                        doc.profilePhoto
                                          ? doc.profilePhoto
                                          : require('assets/img/dashboard/default-avatar.jpg')
                                      }
                                      alt="avatar"
                                    />
                                  </Col>
                                  <Col
                                    xl="4"
                                    lg="4"
                                    md="12"
                                    sm="12"
                                    xs="12"
                                    className="text-center text-lg-left"
                                  >
                                    <h3 className="mb-0">
                                      {doc.firstName} {doc.lastName}
                                    </h3>
                                    <h5 className="text-muted font-weight-400 mb-0">
                                      {doc.speciality}
                                    </h5>
                                    <h3 className="mb-0 text-success">
                                      ${crypto.getRandomValues(array)}
                                    </h3>
                                  </Col>
                                  <Col
                                    xl="6"
                                    lg="5"
                                    md="12"
                                    sm="12"
                                    xs="12"
                                    className="text-center text-lg-right mt-3 mt-lg-0"
                                  >
                                    <Button color="primary">Choose</Button>
                                  </Col>
                                </Row>
                              </CardBody>
                            </Card>
                          </Col>
                        )
                      }) : speciality ? <Col><p className='text-sm'>No doctors found for this speciality.</p></Col> : null}
                    </Row>
                  </Col>
                  <Col lg="6" md="12" sm="12" xs="12" className="text-left">
                    <h3 className="mt-3">Select Date</h3>
                    <Calendar
                      calendarClassName="w-100"
                      value={null}
                      onChange={(value) => console.log(value)}
                      shouldHighlightWeekends
                    />
                    <h3 className="mt-3">Choose a time range</h3>
                  </Col>
                </Row>
              </CardBody>
              <CardFooter className="border-0 pt-0">
                <Row>
                  <Col className="text-center">
                    <Button color="primary btn-lg">Create Appointment</Button>
                  </Col>
                </Row>
              </CardFooter>
            </Card>
          </Col>
        </Row>
      </Container>
    </>
  )
}

export default CreateAppointment
