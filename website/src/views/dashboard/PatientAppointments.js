import {
    Container,
    Row,
    Col,
    Card,
    CardHeader,
    Button,
    CardBody,
    Table, 
    Input,
    Label
} from 'reactstrap'
// core components
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from 'react'
import Header from 'components/Headers/Header'
import { useUserContext } from "context/UserContext";
import AppointmentApi from 'api/appointments';
import moment from 'moment';
import Swal from 'sweetalert2'
import withReactContent from 'sweetalert2-react-content'
import PrescriptionApi from 'api/prescription';
const ReactSwal = withReactContent(Swal)

const PatientAppointments = () => {

    const [error, setError] = useState('')
    const [appointments, setAppointments] = useState([])
    const { user } = useUserContext()

    useEffect(() => {
        const fetchData = async () => {
            await getAppointments()
        }
        fetchData()
    }, [])

    useEffect(() => {
        appointments.sort((a, b) => new Date(...a.date.split('-').reverse()) - new Date(...b.date.split('-').reverse()));
        appointments.sort((a, b) => a.date.split(":") - b.date.split(":"));
    }, [])
    const getAppointments = async () => {
        try {
            const response = await AppointmentApi.GetPatientAppointments(user.id)
            console.log(response)
            setAppointments(response.data)
        } catch (error) {
            console.log(error)
            setError("Error: Couldn't get the appointments.")
        }
    }

    const showPrescription = async (appointment) => {
        ReactSwal.fire({
            showCancelButton: false,
            showCloseButton: false,
            showConfirmButton: false,
            html: <Prescription appointment={appointment} close={ReactSwal.close} />
        })
    }
    return (
        <>
            <Header />
            {/* Page content */}
            <Container className="mt--7" fluid>
                {/* Table */}
                <Row>
                    <Col>
                        <Card className="shadow">
                            <CardHeader>
                                <Row className="align-items-center">
                                    <Col lg="8" md="8" sm="12" xs="12" className="text-left">
                                        <h3 className="mb-0">
                                            View your appointments
                                        </h3>
                                    </Col>
                                </Row>
                            </CardHeader>
                            <CardBody>
                                <Row>
                                    <Table className="align-items-center table-flush" responsive>
                                        <thead className="thead-light">
                                            <tr>
                                                <th scope="col">Date</th>
                                                <th scope="col">Time</th>
                                                <th scope="col">Doctor</th>
                                                <th scope="col">Email</th>
                                                <th scope='col'>Prescription</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            {error ? <tr><td><h4 className='text-left text-muted'>Sorry, we couldn't get the appointments list.</h4></td></tr> : null}
                                            {appointments.map((appointment, index) => {
                                                return (
                                                    <tr key={index}>
                                                        <th scope="row">
                                                            <span className="mb-0 text-sm">
                                                                {moment(appointment.date).diff(new Date().toISOString().slice(0, 10), 'day') === 0 ? "Today" : moment(appointment.date).diff(new Date().toISOString().slice(0, 10), 'day') === 1 ? "Tomorrow" : appointment.date}
                                                            </span>
                                                        </th>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.startTime} - {appointment.endTime}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.doctorFirstName} {appointment.doctorLastName}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.email}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <Button className="btn btn-info btn-sm" onClick={showPrescription({appointmentId: appointment.id, appointmentDate: appointment.date})}>
                                                                Prescription
                                                            </Button>
                                                        </td>
                                                    </tr>)
                                            })}
                                        </tbody>
                                    </Table>
                                </Row>
                            </CardBody>
                        </Card>
                    </Col>
                </Row>
            </Container>
        </>
    )
}

export default PatientAppointments

const Prescription = (props) => {
    const [error, setError] = useState("")
    const [prescription, setPrescription] = useState({})
    const { user } = useUserContext()


    useEffect(() => {
        const fetchData = async () => {
            await getPrescription()
        }
        fetchData()
    }, [user])

    const getPrescription = async () => {
        try {
            console.log("appointmentId ", props.appointment.appointmentId)
            const response = await PrescriptionApi.getPrescriptionByAppointmentId(props.appointment.appointmentId)
            setPrescription(response.data)
        } catch (err) {
            setError("Error: Couldn't get the drug list.")
        }
    }
    return <>
        <Row>
            <Col>
                <Row className='align-items-center mb-1'>
                    <Col className='text-left'>
                        <h3>Your prescription</h3>
                    </Col>
                    <Col className='text-right' xs="2">
                        <button aria-label="Close" className='close' data-dismiss="modal" type='button' onClick={props.close}>
                            <span aria-hidden={true}>x</span>
                        </button>
                    </Col>
                </Row>
                <hr className='mt-3' />
                {moment(props.appointment.date).diff(new Date().toISOString().slice(0, 10), 'day') < 0 ? 
                <>
                <Row className='mb-2'>
                    <Col className='text-left'>
                        <h3>Diagnostic: </h3>
                    </Col>
                </Row>
                <Row className='mb-2'>
                    <Col className='text-left'>
                        <h3>Description: </h3>
                    </Col>
                </Row>
                <Row>
                    <Col className='text-left'>
                        <h3>Drugs to take:</h3>
                    </Col>
                </Row>
                <Row className='mt-4'>
                    <Col className='text-left'>
                        <h3>Procedure</h3>
                    </Col>
                </Row>
                <Row>
                    <Col className='text-left'>
                        <p>Name: </p>
                    </Col>
                </Row>
                <Row>
                    <Col className='text-left'>
                        <p>Description: </p>
                    </Col>
                </Row>
                <Row>
                    <Col className='text-left'>
                        <p>Price: </p>
                    </Col>
                </Row>
                </>
                 : <h4>You don't have any prescription for this appointment</h4>}
            </Col>
        </Row>
    </>
       
}

