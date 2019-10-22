import React, { Component } from 'react';
import styled from 'styled-components';

const StyledButton = styled.button`
  border: thin solid ${props => props.color.secondary || "#000000"};
  background-color: ${props => props.color.primary || "#FFFFFF"};
  color: ${props => props.color.secondary || "#000000"};
  text-transform: ${props => props.uppercase ? 'uppercase' : 'none'};
  padding: ${props => props.padding || "10px"};
  transition: background-color 0.5s, color 0.5s, border-color 0.5s;
  border-radius: ${props => props.radius || '5px'};
  margin: ${props => props.margin || '0px'};
  font-size: ${props => props.fontsize || '18px'};
  outline: none;

  &:hover{
    border-color: ${props => props.color.primary || "#000000"};
    background-color: ${props => props.color.secondary || "#FFFFFF"};
    color: ${props => props.color.primary || "#000000"};
  }

  &[disabled="true"] {
    background-color: gray;
  }
`

export class Button extends Component {
  displayName = Button.name

  render() {

    return (
      <StyledButton
        {...this.props}
        onClick={this.props.onClick}
        type={this.props.type || "button"}
      >
        {this.props.children}
      </StyledButton>
    );
  }
}