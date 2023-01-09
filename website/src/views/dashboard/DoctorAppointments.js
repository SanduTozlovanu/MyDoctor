import {
    Container,
    Row,
    Col,
    Card,
    CardHeader,
    CardBody,
    Table,
    Button,
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
const ReactSwal = withReactContent(Swal)

const DoctorAppointments = () => {

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
            const response = await AppointmentApi.GetDoctorAppointments(user.id)
            console.log(response)
            setAppointments(response.data)
        } catch (error) {
            console.log(error)
            setError("Error: Couldn't get the appointments.")
        }
    }

    const createPrescription = async (patient) => {
        ReactSwal.fire({
            html: <Prescription patient={patient} close={ReactSwal.close} />
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
                                                <th scope="col">Name</th>
                                                <th scope="col">Email</th>
                                                <th scope="col">Prescription</th>
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
                                                                {appointment.patientFirstName} {appointment.patientLastName}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <span className="mb-0 text-sm">
                                                                {appointment.email}
                                                            </span>
                                                        </td>
                                                        <td>
                                                            <Button size='sm' color="info" className='text-center' onClick={() => createPrescription({ firstName: appointment.patientFirstName, lastName: appointment.patientLastName, email: appointment.email })}>Prescription</Button>
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

export default DoctorAppointments

const Prescription = (props) => {
    const [test, setTest] = useState("");

    return (
        <>
        <h2>title: {test}</h2>
        <input onChange={e => setTest(e.target.value)} />
    </>
    )
}
