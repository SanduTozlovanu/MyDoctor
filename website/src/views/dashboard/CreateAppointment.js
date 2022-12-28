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
  Button,
  CardFooter,
} from 'reactstrap'
import 'react-modern-calendar-datepicker/lib/DatePicker.css'
import { Calendar } from 'react-modern-calendar-datepicker'
import Select from 'react-select'
import DoctorApi from 'api/doctor'
// core components
import Header from 'components/Headers/Header.js'


function getDayName(date) {
  const locale = 'en-US'
  return date.toLocaleDateString(locale, {weekday: 'long'});
}

const CreateAppointment = () => {
  const [speciality, setSpeciality] = useState('')
  const [specialities, setSpecialities] = useState([])
  const [error, setError] = useState('')
  const [doctors, setDoctors] = useState([])
  const [doctor, setDoctor] = useState({})
  const [scheduleIntervals, setScheduleIntervals] = useState([])
  const [appointmentInterval, setAppointmentInterval] = useState({})
  const [appointmentDay, setAppointmentDay] = useState({})
  const [appointmentDayName, setAppointmentDayName] = useState("")

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
    console.log(doctors)
  }, [speciality])

  // const handleSpeciality = (spec, info) => {
  //   console.log(spec, info)
  //   if (info.action === 'clear') {
  //     console.log("clear")
  //     setDoctors([])
  //     setDoctor({})
  //   } else {
  //     console.log("select")
  //     setSpeciality(spec ? spec.value : '')
  //     console.log(speciality)
  //   }
  // }

  const getScheduleIntervals = async (data) =>{
    try{
      const response = await DoctorApi.GetAvailableAppointmentSchedule(doctor.id, data)
      setScheduleIntervals(response.data)
      setAppointmentDay(data)
      const newDate = data.year + "-" + data.month + "-" + data.day
      setAppointmentDayName(getDayName(new Date(newDate)))
    }catch(error){
      console.log(error)
    }
  }

useEffect(()=> {
  console.log(appointmentDayName)
}, [appointmentDayName])
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
                  <Col
                    lg="6"
                    md="12"
                    sm="12"
                    xs="12"
                    className="text-left"
                    style={{ zIndex: '100' }}
                  >
                    <h3>Select a speciality</h3>
                    <Select
                      onChange={(spec, action) =>
                        {
                          if (action.action === "select-option" && spec){
                            setSpeciality(spec.value)
                          } else if(action.action === "clear"){
                            setDoctors([])
                            setDoctor({})
                          }
                        }
                      }
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
                  </Col>
                </Row>
                <Row>
                  <Col>
                    {doctors?.length ? (
                      <h3 className="mt-3">Choose your Doctor</h3>
                    ) : null}
                    <Row>
                      {doctors && doctors.length ? (
                        doctors.map((doc, index) => {
                          let array = new Uint32Array(1)
                          return (
                            <Col
                              className="text-left mb-3"
                              xl="3"
                              lg="4"
                              md="4"
                              sm="6"
                              xs="6"
                              key={index}
                            >
                              <Card
                                className={`shadow ${
                                  doc.id === doctor.id
                                    ? 'border border-primary'
                                    : ''
                                } `}
                              >
                                <CardBody>
                                  <Row className="align-items-center">
                                    <Col className="text-center">
                                      <img
                                        className="rounded-circle avatar-lg"
                                        src={
                                          doc.profilePhoto
                                            ? doc.profilePhoto
                                            : require('assets/img/dashboard/default-avatar.jpg')
                                        }
                                        alt="avatar"
                                      />
                                      <h3 className="mb-0 mt-1">
                                        {doc.firstName} {doc.lastName}
                                      </h3>
                                      <h5 className="text-muted font-weight-400 mb-0 mt-2">
                                        {doc.speciality}
                                      </h5>
                                      <h3 className="mb-0 text-success">
                                        $
                                        {crypto.getRandomValues(array) < 200
                                          ? crypto.getRandomValues()
                                          : 150}
                                      </h3>
                                      <Button
                                        color="primary"
                                        className="text-center mt-3"
                                        onClick={() => setDoctor(doc)}
                                      >
                                        Choose
                                      </Button>
                                    </Col>
                                  </Row>
                                </CardBody>
                              </Card>
                            </Col>
                          )
                        })
                      ) : speciality ? (
                        <Col>
                          <p className="text-sm mt-2">
                            No doctors found for this speciality.
                          </p>
                        </Col>
                      ) : null}
                    </Row>
                  </Col>
                </Row>
                <Row className={`${doctor?.id ? 'd-initial' : 'd-none'}`}>
                  <Col
                    lg="6"
                    md="12"
                    sm="12"
                    xs="12"
                    className="text-left mt-3"
                  >
                    <h3>Select Date</h3>
                    <Calendar
                      calendarClassName="w-100"
                      value={null}
                      onChange={(value) => getScheduleIntervals(value)}
                      shouldHighlightWeekends
                    />
                    {scheduleIntervals && scheduleIntervals.length ? (
                    <h3 className="mt-3">Choose a time range</h3>
                    )
                    : <h3 className="mt-3">We don't have any availabily for this day.</h3>}
                    <Row>
                    {scheduleIntervals && scheduleIntervals.length ? (scheduleIntervals.map((interval, index) => {
                      console.log(interval)
                      return (
                        <Col
                          className="text-left mb-3"
                          xl="3"
                          lg="4"
                          md="4"
                          sm="6"
                          xs="6"
                          key={index}
                        >
                          <Card className='c-pointer' onClick={() => setAppointmentInterval({startTime: interval.startTime, endTime: interval.endTime})}>
                            <CardBody>
                              <div>{interval.startTime} - {interval.endTime}</div>
                            </CardBody>
                          </Card>
                        </Col>
                    )})) : null} 
                    </Row>                  
                  </Col>
                </Row>
              </CardBody>
              <CardFooter className="border-0 pt-0">
                <Row>
                  <Col className="text-center">
                    <Button color="primary btn-lg" disabled={doctor && doctor.id && appointmentInterval.startTime && appointmentDay.year? false: true}>
                      Create Appointment
                    </Button>
                  </Col>
                  {error ? (
                    <h4 className="text-center text-danger mt-3 font-weight-400">
                      {error}
                    </h4>
                  ) : null}
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
