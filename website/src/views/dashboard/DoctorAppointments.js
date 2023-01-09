import {
    Container,
    Row,
    Col,
    Card,
    CardHeader,
    CardBody,
    Table,
    Button,
    Input,
    Label
} from 'reactstrap'
// core components
/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from 'react'
import Header from 'components/Headers/Header'
import { useUserContext } from "context/UserContext";
import AppointmentApi from 'api/appointments';
import DrugApi from 'api/drug';
import ProcedureApi from 'api/procedure';
import PrescriptionApi from 'api/prescription';
import moment from 'moment';
import Swal from 'sweetalert2'
import Select from 'react-select'
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
            showCancelButton: false,
            showCloseButton: false,
            showConfirmButton: false,
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
                                                console.log(appointment)
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
                                                            <Button size='sm' color="info" className='text-center' onClick={() => createPrescription({ firstName: appointment.patientFirstName, lastName: appointment.patientLastName, email: appointment.email, appointmentId: appointment.id})}>Prescription</Button>
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
    const [patientName, setPatientName] = useState("")
    const [description, setDescription] = useState("")
    const [drugs, setDrugs] = useState([])
    const [procedures, setProcedures] = useState([])
    const [error, setError] = useState("")
    const [chosenDrugs, setChosenDrugs] = useState([])
    const [chosenProcedures, setChosenProcedures] = useState([])
    useEffect(() => {
        setPatientName(props.patient.firstName + " " + props.patient.lastName)
        console.log(patientName)
    }, [props])

    useEffect(() => {
        const fetchData = async () => {
            await getDrugs()
            await getProcedures()
        }
        fetchData()
    }, [])

    const getDrugs = async () => {
        try {
            const response = await DrugApi.GetDrugs()
            console.log("drugs ", response.data)
            setDrugs(response.data)
        } catch (err) {
            setError("Error: Couldn't get the drug list.")
        }
    }

    const getProcedures = async () => {
        try {
            const response = await ProcedureApi.GetProcedures()
            console.log("procedures ", response.data)
            setProcedures([{name: "consult", "description:" : "aa", price: 100}, {name: "consult1", "description:" : "aa", price: 13}])
        } catch (err) {
            setError("Error: Couldn't get the procedures list.")
        }
    }

    const sendPrescription = async () => {
        try {
            const data = {
                name: patientName,
                description: description,
                drugs: chosenDrugs,
                procedures: chosenProcedures
            }
            const response = await PrescriptionApi.CreatePrescription(props.appointmentId, data)
            console.log(response.data)
        } catch (error) {
            setError("Error: Couldn't create the prescription.")
        }
    }
    useEffect(()=> {
        console.log(description)
        console.log(chosenDrugs)
        console.log(chosenProcedures)
    })
    return <>
        <Row>
            <Col>
                <Row className='align-items-center mb-1'>
                    <Col className='text-left'>
                        <h3>Create prescription</h3>
                    </Col>
                    <Col className='text-right' xs="2">
                        <button aria-label="Close" className='close' data-dismiss="modal" type='button' onClick={props.close}>
                            <span aria-hidden={true}>x</span>
                        </button>
                    </Col>
                </Row>
                <hr className='mt-3' />
                <Row className='mb-2'>
                    <Col className='text-left'>
                        <h3 className='mb-0'>Patient: {props.patient.firstName} {props.patient.lastName}</h3>
                    </Col>
                </Row>
                <Row className='mb-2'>
                    <Col className='text-left'>
                        <Label
                            className='text-sm'
                        >
                            Description*
                        </Label>
                        <Input
                            defaultValue=""
                            placeholder="Write a description..."
                            type="text"
                            onChange={(e) => setDescription(e.target.value)}
                        />
                    </Col>
                </Row>
                <Row>
                    <Col className='text-left'>
                        <Label className='text-sm'>Select drug</Label>
                        <Select
                            onChange={(drug, action) => {
                                if (action.action === 'select-option' && drug) {
                                    setChosenDrugs(drug.value)
                                } else if (action.action === 'clear') {
                                    setChosenDrugs([])
                                    setError('')
                                }
                            }}
                            defaultValue={null}
                            isSearchable
                            isClearable
                            isMulti
                            name="drug-select"
                            options={drugs.map((item) => {
                                return { label: item.name, value: item.id }
                            })}
                            className="basic-multi-select"
                            classNamePrefix="select"
                        />
                    </Col>
                </Row>
                <Row className='mb-2'>
                    <Col className='text-left'>
                        <Label className='text-sm'>Select procedure</Label>
                        <Select
                            onChange={(procedure, action) => {
                                if (action.action === 'select-option' && procedure) {
                                    setChosenProcedures(procedure.value)
                                } else if (action.action === 'clear') {
                                    setChosenProcedures([])
                                    setError('')
                                }
                            }}
                            defaultValue={null}
                            isSearchable
                            isClearable
                            isMulti
                            name="procedure-select"
                            options={procedures.map((item) => {
                                return { label: item.name, value: item.id }
                            })}
                            className="basic-multi-select"
                            classNamePrefix="select"
                        />
                    </Col>
                </Row>
                <Row className='mt-3'>
                    <Col>
                        <Button className='text-center' color="primary" onClick={sendPrescription}>Create</Button>
                    </Col>
                </Row>
            </Col>
        </Row>
    </>
}
