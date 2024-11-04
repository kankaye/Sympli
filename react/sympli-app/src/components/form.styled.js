import styled from 'styled-components';

export const Form = styled.form`
  display: flex;
  flex-direction: column;
  width: 400px;
  background-color: ${({ theme }) => theme.formBackground};
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.1);
  transition: background-color 0.3s;
`;

export const Label = styled.label`
  margin-bottom: 5px;
  font-weight: bold;
  color: ${({ theme }) => theme.textColor};
`;

export const Input = styled.input`
  padding: 10px;
  margin-bottom: 20px;
  background-color: ${({ theme }) => theme.inputBackground};
  border: 1px solid ${({ theme }) => theme.inputBorder};
  border-radius: 4px;
  color: ${({ theme }) => theme.textColor};
  transition: background-color 0.3s, border-color 0.3s, color 0.3s;
`;

export const Title = styled.h1`
  color: ${({ theme }) => theme.textColor};
  margin-bottom: 20px;
`;