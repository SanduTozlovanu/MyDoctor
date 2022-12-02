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
  Label,
  Spinner,
} from 'reactstrap'
import StepWizard from 'react-step-wizard'
import { useEffect, useState } from 'react'
import Select from 'react-select'
import AuthApi from 'api/auth'
import { useHistory } from 'react-router-dom'

const Register = () => {
  const [accountType, setAccountType] = useState('')
  const [firstName, setFirstName] = useState('')
  const [lastName, setLastName] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [repeatPassword, setRepeatPassword] = useState('')
  const [age, setAge] = useState(0)
  const [error, setError] = useState(null)
  const [passwordStrength, setPasswordStrength] = useState('Low')
  const [showPassword, setShowPassword] = useState(false)
  const [creating, setCreating] = useState(false)

  const [stepWizardRef, setStepWizardRef] = useState(null)
  const history = useHistory()

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

  useEffect(() => {
    setError(null)
  }, [firstName, lastName, email, password, repeatPassword, age])

  const handleRegister = async () => {
    const error = verifyCredentials()
    if (error) {
      return
    }
    if (accountType === 'patient') {
      return await registerPatient()
    } else {
      stepWizardRef.nextStep()
    }
  }

  const verifyCredentials = () => {
    if (creating) {
      return
    }
    if (
      !firstName ||
      !lastName ||
      !email ||
      !password ||
      !repeatPassword ||
      (accountType === 'patient' && !age)
    ) {
      setError('You must fill in all credentials.')
      return true
    }
    if (accountType === 'patient' && age < 14) {
      setError('You must be older than 14.')
      return true
    }
    if (password !== repeatPassword) {
      setError('Password do not match.')
      return true
    }
    if (passwordStrength === 'Low') {
      setError('Please create a stronger password.')
      return true
    }
    return false
  }

  const registerPatient = async () => {
    try {
      setCreating(true)
      const credentials = {
        userDetails: {
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
        },
        age: age,
      }
      const register_response = await AuthApi.RegisterPatient(credentials)
      const user_response = await AuthApi.Login({
        email: email,
        password: password,
      })
      const user = { ...register_response.data, ...user_response.data }
      localStorage.setItem('user', JSON.stringify(user))
      return history.push('/admin/index')
    } catch (err) {
      console.log(err)
      setCreating(false)
      if (err && err.message) {
        return setError(err.message)
      }
      return setError('There has been an error.')
    }
  }

  const handleType = (value) => {
    setAccountType(value)
    stepWizardRef.nextStep()
  }

  return (
    <>
      <Col lg="6" md="8">
        <Card className="bg-secondary shadow border-0">
          <StepWizard transitions={{}} ref={(ref) => setStepWizardRef(ref)}>
            <FirstStep handleType={handleType} />

            <SecondStep
              setFirstName={setFirstName}
              setLastName={setLastName}
              setEmail={setEmail}
              setShowPassword={setShowPassword}
              showPassword={showPassword}
              setRepeatPassword={setRepeatPassword}
              setPassword={setPassword}
              setAge={setAge}
              passwordStrength={passwordStrength}
              error={error}
              handleRegister={handleRegister}
              accountType={accountType}
              stepWizardRef={stepWizardRef}
              creating={creating}
            />
            <ThirdStep
              firstName={firstName}
              lastName={lastName}
              email={email}
              password={password}
              history={history}
            />
          </StepWizard>
        </Card>
      </Col>
    </>
  )
}

const FirstStep = ({ handleType }) => {
  return (
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
  )
}

const SecondStep = ({
  setFirstName,
  setLastName,
  setEmail,
  setShowPassword,
  showPassword,
  setRepeatPassword,
  setPassword,
  setAge,
  passwordStrength,
  error,
  handleRegister,
  accountType,
  creating,
}) => {
  return (
    <>
      <CardBody className="px-lg-5 py-lg-5">
        <div className="text-center text-muted mb-4">
          <h3>Sign up with credentials</h3>
        </div>
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
          {accountType === 'doctor' ? null : (
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
          )}
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
              onClick={handleRegister}
              className="mt-4"
              color="primary"
              type="button"
              disabled={creating}
            >
              {accountType === 'patient' ? (
                <>{creating ? <Spinner size="sm" /> : 'Create account'}</>
              ) : (
                'Next step'
              )}
            </Button>
          </div>
        </Form>
      </CardBody>
    </>
  )
}

const ThirdStep = ({ firstName, lastName, email, password, history }) => {
  const [speciality, setSpeciality] = useState('')
  const [degreePhoto, setDegreePhoto] = useState('')
  const [profilePhoto, setProfilePhoto] = useState('')
  const [error, setError] = useState(null)
  const [creating, setCreating] = useState(false)

  const specialitiesOptions = [
    { value: 'neurologist', label: 'Neurologist' },
    { value: 'orl', label: 'ORL' },
    { value: 'family medicine', label: 'Family medicine' },
    { value: 'internal medicine', label: 'Internal medicine' },
  ]

  function uploadFile(event, type) {
    var blobFile = event.target.files[0]
    const img = new Image()
    var url = window.URL.createObjectURL(blobFile)
    img.src = url
    if (type === 'profile') {
      setProfilePhoto(img.src)
    } else if (type === 'degree') {
      setDegreePhoto(img.src)
    }

    const formData = new FormData()
    formData.append('fileToUpload', blobFile)
    try {
      //api upload img call with formData
    } catch (err) {
      console.log(err)
    }
  }

  const verifyCredentials = () => {
    if (!speciality || !profilePhoto || !degreePhoto) {
      setError('Please fill in all required fields.')
      return true
    }
    return false
  }

  const registerDoctor = async () => {
    const error = verifyCredentials()
    if (error) {
      return
    }
    setCreating(true)
    try {
      const credentials = {
        userDetails: {
          firstName: firstName,
          lastName: lastName,
          email: email,
          password: password,
          profilePhoto: profilePhoto,
          degreePhoto: degreePhoto,
        },
        speciality: speciality,
      }
      const register_response = await AuthApi.RegisterDoctor(credentials)
      const user_response = await AuthApi.Login({
        email: email,
        password: password,
      })
      const user = { ...register_response.data, ...user_response.data }
      localStorage.setItem('user', JSON.stringify(user))
      return history.push('/admin/index')
    } catch (err) {
      console.log(err)
      setCreating(false)
      if (err && err.message) {
        return setError(err.message)
      }
      return setError('There has been an error.')
    }
  }

  return (
    <>
      <CardBody className="px-lg-5 py-lg-5">
        <div className="text-center text-muted mb-4">
          <h3>Let's complete your profile</h3>
        </div>
        <Form role="form">
          <FormGroup className="mb-3">
            <Label>Choose your speciality *</Label>
            <Select
              onChange={(value) => setSpeciality(value ? value.label : '')}
              defaultValue={null}
              isSearchable
              isClearable
              name="speciality"
              options={specialitiesOptions}
              className="basic-single"
              classNamePrefix="select"
            />
          </FormGroup>
          <Row>
            <Col lg="6" md="6" sm="6" xs="12" className="text-left">
              <FormGroup>
                <Label className="ws-0">Upload your Degree photo *</Label>
                <Input
                  accept=".png,.jpg,.jpeg,.svg,.gif"
                  onChange={(e) => uploadFile(e, 'degree')}
                  id="degree-photo"
                  className="d-none"
                  type="file"
                />
                <Label
                  className="c-pointer text-center"
                  style={{ border: '2px dotted gray', borderRadius: '10px' }}
                  htmlFor="degree-photo"
                >
                  <img
                    className="w-75"
                    src={
                      degreePhoto
                        ? degreePhoto
                        : require('assets/img/dashboard/degree-default.png')
                    }
                    alt="Degree"
                  />
                </Label>
              </FormGroup>
            </Col>
            <Col lg="6" md="6" sm="6" xs="12" className="text-left">
              <FormGroup>
                <Label className="ws-0">Upload your profile photo *</Label>
                <Input
                  accept=".png,.jpg,.jpeg,.svg,.gif"
                  onChange={(e) => uploadFile(e, 'profile')}
                  id="profile-photo"
                  className="d-none"
                  type="file"
                />
                <Label
                  className="c-pointer text-center"
                  style={{ border: '2px dotted gray', borderRadius: '10px' }}
                  htmlFor="profile-photo"
                >
                  <img
                    className="w-75"
                    src={
                      profilePhoto
                        ? profilePhoto
                        : require('assets/img/dashboard/profile-default-doctor.png')
                    }
                    alt="Profile"
                  />
                </Label>
              </FormGroup>
            </Col>
          </Row>
          {error ? (
            <h4 className="text-center text-danger mt-3 font-weight-400">
              {error}
            </h4>
          ) : null}
          <div className="text-center">
            <Button
              onClick={registerDoctor}
              className="mt-4"
              color="primary"
              type="button"
              disabled={creating}
            >
              {creating ? <Spinner size="sm" /> : 'Create account'}
            </Button>
          </div>
        </Form>
      </CardBody>
    </>
  )
}

export default Register
