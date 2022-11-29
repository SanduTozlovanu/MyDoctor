/* eslint-disable react-hooks/exhaustive-deps */
/*!

=========================================================
* Argon Dashboard React - v1.2.2
=========================================================

* Product Page: https://www.creative-tim.com/product/argon-dashboard-react
* Copyright 2022 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/argon-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/

// reactstrap components
import {
  Button,
  Card,
  CardBody,
  FormGroup,
  Form,
  Input,
  InputGroupAddon,
  InputGroupText,
  InputGroup,
  Row,
  Col,
} from 'reactstrap';
import StepWizard from 'react-step-wizard';
import { useEffect, useState } from 'react';
import axios from 'axios';

const Register = () => {
  const [accountType, setAccountType] = useState('')
  const [stepWizardRef, setStepWizardRef] = useState(null)

  const handleType = (value) => {
    setAccountType(value)
    console.log(value)
    stepWizardRef.nextStep()
  }
  return (
    <>
      <Col lg="6" md="8">
        <Card className="bg-secondary shadow border-0">
          <StepWizard transitions={{}} ref={(ref) => setStepWizardRef(ref)}>
            <>
              <CardBody className="px-lg-5 py-lg-5">
                <Row>
                  <Col>
                    <h3 className="text-center">Choose your Account type</h3>
                    <Row className="mt-4">
                      <Col>
                        <div
                          onClick={() => handleType('patient')}
                          className="border border-info register-box"
                        >
                          <i
                            className="fas fa-user text-info"
                            style={{ fontSize: '45px' }}
                          />
                          <h1 className="display-4 mt-3">I'm a patient</h1>
                        </div>
                      </Col>
                      <Col>
                        <div
                          onClick={() => handleType('doctor')}
                          className="border border-primary register-box"
                        >
                          <i
                            className="fas fa-user-md text-primary"
                            style={{ fontSize: '45px' }}
                          />
                          <h1 className="display-4 mt-3">I'm a doctor</h1>
                        </div>
                      </Col>
                    </Row>
                  </Col>
                </Row>
              </CardBody>
            </>
            <SecondStep
              accountType={accountType}
              stepWizardRef={stepWizardRef}
            />
          </StepWizard>
        </Card>
      </Col>
    </>
  )
}

export default Register

const SecondStep = (props) => {
  const [componentToRender, setComponentToRender] = useState(null)

  useEffect(() => {
    switch (props.accountType) {
      case 'patient':
        setComponentToRender(<RegisterPatient />)
        break
      case 'doctor':
        setComponentToRender(<RegisterDoctor />)
        break
      default:
        setComponentToRender(
          <h1>Something went wrong. Try refreshing the page.</h1>,
        )
    }
  }, [props.accountType])

  return (
    <>
      <CardBody className="px-lg-5 py-lg-5">
        <div className="text-center text-muted mb-4">
          <h3>Sign up with credentials</h3>
        </div>

        {componentToRender}
      </CardBody>
    </>
  )
}

const RegisterPatient = () => {
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [repeatPassword, setRepeatPassword] = useState('')
  const [age, setAge] = useState(0)
  const [error, setError] = useState(null)
  const [passwordStrength, setPasswordStrength] = useState('Low')
  const [showPassword, setShowPassword] = useState(false)

  var strongRegex = new RegExp(
    '(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.{8,50})',
  )
  var mediumRegex = new RegExp(
    '(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z]))|((?=.*[A-Z])))(?=.{6,50})',
  )

  useEffect(() => {
    if (strongRegex.test(password)) {
      setPasswordStrength('Strong')
    } else if (mediumRegex.test(password)) {
      setPasswordStrength('Medium')
    } else {
      setPasswordStrength('Low')
    }
  }, [password])

  const register = () => {
    if (
      !firstName ||
      !lastName ||
      !email ||
      !password ||
      !repeatPassword ||
      !age
    ) {
      return setError('You must fill in all credentials.')
    }
    if (age < 14) {
      return setError('You must be older than 14.')
    }
    if (password !== repeatPassword) {
      return setError('Password do not match.')
    }
    if (passwordStrength === 'Low') {
      return setError('Please create a stronger password.')
    }
    try {
      axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
      axios.post('https://localhost:7244/api/Patients', {
        firstName: firstName,
        lastName: lastName,
        email: email,
        password: password,
        age: age
      })
      .then(function (response) {
        console.log(response);
      })
      .catch(function (error) {
        console.log(error);
        return setError(error.message);
      });

    } catch (err) {
      console.log(err)
      return setError('There has been an error.')
    }
  }

  return (
    <>
      <Form role="form">
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-hat-3" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setFirstName(e.target.value)}
              placeholder="First Name"
              type="text"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-hat-3" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setLastName(e.target.value)}
              placeholder="Last Name"
              type="text"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-email-83" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setEmail(e.target.value)}
              placeholder="Email"
              type="email"
              autoComplete="new-email"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i
                  onClick={() => setShowPassword(!showPassword)}
                  className={
                    showPassword
                      ? 'fas fa-eye-slash c-pointer'
                      : 'fas fa-eye c-pointer'
                  }
                />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setPassword(e.target.value)}
              placeholder="Password"
              type={showPassword ? 'text' : 'password'}
              autoComplete="new-password"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i
                  onClick={() => setShowPassword(!showPassword)}
                  className={
                    showPassword
                      ? 'fas fa-eye-slash c-pointer'
                      : 'fas fa-eye c-pointer'
                  }
                />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setRepeatPassword(e.target.value)}
              placeholder="Confirm password"
              type={showPassword ? 'text' : 'password'}
              autoComplete="new-password"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-badge" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              onChange={(e) => setAge(Math.abs(e.target.value))}
              placeholder="Age"
              type="number"
            />
          </InputGroup>
        </FormGroup>
        <div className="text-muted font-italic">
          <small>
            Password strength:{' '}
            <span
              className={`${
                passwordStrength === 'Low'
                  ? 'text-danger'
                  : passwordStrength === 'Medium'
                  ? 'text-warning'
                  : 'text-success'
              } font-weight-700`}
            >
              {passwordStrength}
            </span>
          </small>
        </div>
        <Row className="my-4">
          <Col xs="12">
            <div className="custom-control custom-control-alternative custom-checkbox">
              <input
                className="custom-control-input"
                id="customCheckRegister"
                type="checkbox"
              />
              <label
                className="custom-control-label"
                htmlFor="customCheckRegister"
              >
                <span className="text-muted">
                  I agree with the{' '}
                  <a href="#pablo" onClick={(e) => e.preventDefault()}>
                    Privacy Policy
                  </a>
                </span>
              </label>
            </div>
          </Col>
        </Row>
        {error ? (
          <h4 className="text-center text-danger mt-3 font-weight-400">
            {error}
          </h4>
        ) : null}
        <div className="text-center">
          <Button
            onClick={register}
            className="mt-4"
            color="primary"
            type="button"
          >
            Create account
          </Button>
        </div>
      </Form>
    </>
  )
}

const RegisterDoctor = () => {
  return (
    <>
      <Form role="form">
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-hat-3" />
              </InputGroupText>
            </InputGroupAddon>
            <Input placeholder="First Name" type="text" />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-hat-3" />
              </InputGroupText>
            </InputGroupAddon>
            <Input placeholder="Last Name" type="text" />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative mb-3">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-email-83" />
              </InputGroupText>
            </InputGroupAddon>
            <Input placeholder="Email" type="email" autoComplete="new-email" />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-lock-circle-open" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              placeholder="Password"
              type="password"
              autoComplete="new-password"
            />
          </InputGroup>
        </FormGroup>
        <FormGroup>
          <InputGroup className="input-group-alternative">
            <InputGroupAddon addonType="prepend">
              <InputGroupText>
                <i className="ni ni-lock-circle-open" />
              </InputGroupText>
            </InputGroupAddon>
            <Input
              placeholder="Confirm password"
              type="password"
              autoComplete="new-password"
            />
          </InputGroup>
        </FormGroup>
        <div className="text-muted font-italic">
          <small>
            password strength:{' '}
            <span className="text-success font-weight-700">strong</span>
          </small>
        </div>
        <Row className="my-4">
          <Col xs="12">
            <div className="custom-control custom-control-alternative custom-checkbox">
              <input
                className="custom-control-input"
                id="customCheckRegister"
                type="checkbox"
              />
              <label
                className="custom-control-label"
                htmlFor="customCheckRegister"
              >
                <span className="text-muted">
                  I agree with the{' '}
                  <a href="#pablo" onClick={(e) => e.preventDefault()}>
                    Privacy Policy
                  </a>
                </span>
              </label>
            </div>
          </Col>
        </Row>
        <div className="text-center">
          <Button className="mt-4" color="primary" type="button">
            Create account
          </Button>
        </div>
      </Form>
    </>
  )
}
