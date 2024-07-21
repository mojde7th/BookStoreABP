import axios from 'axios';

const API_URL = 'https://localhost:44355' + '/api/publishers';

export const getPublishers = () => axios.get(API_URL);
export const getPublisher = (id) => axios.get(`${API_URL}/${id}`);
export const createPublisher = (publisher) => axios.post(API_URL, publisher);
export const updatePublisher = (id, publisher) => axios.put(`${API_URL}/${id}`, publisher);
export const deletePublisher = (id) => axios.delete(`${API_URL}/${id}`);
