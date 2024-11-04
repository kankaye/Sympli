import styled from 'styled-components';

export const Button = styled.button`
  padding: 12px;
  background-color: ${({ theme }) => theme.buttonBackground};
  color: #fff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  transition: background-color 0.3s;

  &:hover {
    background-color: ${({ theme }) => theme.buttonHoverBackground};
  }

  &:disabled {
    background-color: #9e9e9e;
    cursor: not-allowed;
  }
`;