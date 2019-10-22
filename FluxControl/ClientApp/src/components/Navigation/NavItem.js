import React, { Component } from 'react';
import styled, { css } from 'styled-components'
import { Link } from 'react-router-dom';

import { Icon } from '../Shared/Icon';

const ContainerMixin = css`
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 10px;
  height: 40px;
  width: 100%;

  div:first-child {
    display: flex;
    justify-content: flex-start;
    align-items: center;
    width: 100%;
    max-width: 50%;

    span {
      margin-left: 15px;
    }
  }
`

const Container = styled.div`
  ${ContainerMixin}
  ${props => props.withChildren ? 'cursor: pointer;' : 'margin-top: 10px'}
  background-color: #F2F2F2;

  span {
    color: #17478F;
  }
`

const ContainerLink = styled(Link)`
  ${ContainerMixin}
  background-color: #FFFFFF;
  transition: background-color 0.5s;

  &:focus, &:hover, &:visited, &:link, &:active {
    text-decoration: none;
  }

  div:first-child {

    span 
      color: #438EFF;
      transition: color 0.5s;
    }
  }
  
  &:hover {
    background-color: #438EFF;
    
    div:first-child {

      span {
        color: #FFFFFF;
      }
    }
  }
`

const Title = styled.span`
  font-size: 18px;
`

const Line = styled.hr`
  width: 30%;
  border-top: thin solid #17478F;
`
const ContainerOfLinks = styled.div`
  width: 100%;
  overflow: hidden;
  transition: max-height 0.5s ease-in-out;
`

const ArrowDiv = styled.div`
  transition: transform 0.5s;
  padding: 0;
  margin: 0;
  display: flex;
`

export class NavItem extends Component {
  displayName = NavItem.name

  constructor(props) {
    super(props);
    this.state = {
      deg: 0,
      linksContainerPx: '0',
      isOpen: false
    };
    this.openAndClose = this.openAndClose.bind(this);
  }

  openAndClose() {
    let deg = this.state.isOpen ? 0 : 90;
    let linksContainerPx = this.state.isOpen ? '0px' : '40px';
    let isOpen = this.state.isOpen ? false : true;
    
    this.setState({
      deg: deg,
      linksContainerPx: linksContainerPx,
      isOpen: isOpen
    });
  }

  render() {
    const children = this.props.children;

    return (
      <React.Fragment>
        <Container
          withChildren={children ? true : false}
          onClick={children ? this.openAndClose : false}
        >
          <div>
            <Icon icon={this.props.icon} fill="#17478F" />
            <Title> {this.props.title} </Title>
          </div>
          {children ?
            <ArrowDiv style={{ transform: `rotate(${this.state.deg}deg)` }}>
              <Icon icon="ARROW" height="18px" fill="#17478F" />
            </ArrowDiv>
            : <Line />
          }
        </Container>
        {children &&
          <ContainerOfLinks style={{ maxHeight: this.state.linksContainerPx }}>
            {children}
          </ContainerOfLinks>
        }
      </React.Fragment>
    );
  }
}

export class NavLink extends Component {
  displayName = NavLink.name

  constructor(props) {
    super(props)
    this.state = {
      hover: false
    }

    this.toggleHover = this.toggleHover.bind(this);
  }

  toggleHover() {
    this.setState({ hover: !this.state.hover });
    this.refs.child.hover(!this.state.hover, '#FFFFFF');
  }

  render() {

    return (
      <ContainerLink to={this.props.to} onMouseEnter={this.toggleHover} onMouseLeave={this.toggleHover}>
        <div>
          <Icon icon={this.props.icon} heigth="35px" fill="#438EFF" ref="child" />
          <Title> {this.props.title} </Title>
        </div>
      </ContainerLink>
    );
  }
}
