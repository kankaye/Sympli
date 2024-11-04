import React from 'react';
import styled, { keyframes } from 'styled-components';

const rotate = keyframes`
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
`;

const SpinnerDiv = styled.div`
  border: 4px solid ${({ theme }) => theme.inputBorder};
  border-top: 4px solid ${({ theme }) => theme.buttonHoverBackground};
  border-radius: 50%;
  width: 20px;
  height: 20px;
  animation: ${rotate} 1s linear infinite;
  display: inline-block;
  margin-left: 10px;
`;

const Spinner = () => <SpinnerDiv />;

export default Spinner;
