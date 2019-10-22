import React, { Component } from 'react';
import styled from 'styled-components';

import { Icon } from '../Shared/Icon';
import { Link } from 'react-router-dom';

const Container = styled(Link)`
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100%;
  height: 60px;
  background-color: #485D7D;
`

export class NavLogo extends Component {
  displayName = NavLogo.name

  render() {

    return (
      <Container to={'/'}>
        <Icon icon="LOGO" height='35px' fill="#FFFFFF" />
      </Container>
    );
  }
}