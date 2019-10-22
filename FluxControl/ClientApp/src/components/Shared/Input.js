import React, { Component } from 'react';
import styled, { css } from 'styled-components';

const Label = styled.label`
  font-size: 14px;
  color: #3D3D3D;
  font-weight: 600;
  width: 100%;
`

const InputMixin = css`
  width: 100%;
  background-color: #FFFFFF;
  border: none;
  border-radius: 5px;
  box-shadow: 0px 0px 5px 0px rgba(0,0,0,0.45);
  font-size: 18px;
  font-weight: 400;
  padding: 6px 12px;
  margin-top: 5px;
  outline: none;

  &.error {
    border: thin solid red;
  }

  label.error {
    color: red;
  }
`

const InputText = styled.input`
  ${InputMixin}
`

const InputSelect = styled.select`
  ${InputMixin}
  padding: 9px 12px;
`

export class Input extends Component {
  displayName = Input.name;

  render() {

    const Context = this.props.context;

    return (
      <Context.Consumer>
        {({ handleValue }) =>
          <Label>
            {this.props.label}
            {this.props.type === 'select' ? 
              <InputSelect
                name={this.props.name}
                onChange={handleValue}
                value={this.props.value || ""}
                className={this.props.error != null ? "error" : ""}
              >
                {this.props.children}
              </InputSelect>
              :
              <InputText
                name={this.props.name}
                type={this.props.type || "Text"}
                onChange={handleValue}
                value={this.props.value || ""}
                className={this.props.error != null ? "error" : ""}
              />
            }
            {this.props.error != null &&
              <label className="error">{this.props.error}</label>
            }
          </Label>
        }
      </Context.Consumer>        
    );
  }
}