import React from 'react'
import { shallow } from 'enzyme'
import Login from '../../views/examples/Login'
it('should test AdminFooter component', () => {
  const wrapper = shallow(<Login />)
  expect(wrapper).toMatchSnapshot()
})
