import React, { useEffect, useState } from 'react'
import { Route } from 'react-router-dom'
import { useUserContext } from 'context/UserContext'
import { useHistory } from 'react-router-dom'

const ProtectedRoute = ({ component: Component, ...restOfProps }) => {
  const [loading, setLoading] = useState(true)
  const { user } = useUserContext()
  const history = useHistory()

  useEffect(() => {
    setLoading(false)
  }, [user])

  useEffect(() => {
    if (loading) {
      return
    }
    if (!user || !user.jwtToken) {
      return history.push('/auth/login')
    }
  }, [history, loading, user])

  return <Route {...restOfProps} render={(props) => <Component {...props} />} />
}

export default ProtectedRoute
