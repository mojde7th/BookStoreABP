import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { createPublisher } from '../services/publisherService';

const PublisherForm = () => {
  const [publisher, setPublisher] = useState({ name: '' });
  const navigate = useNavigate();

  const handleChange = e => {
    const { name, value } = e.target;
    setPublisher(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleSubmit = e => {
    e.preventDefault();
    createPublisher(publisher)
      .then(() => navigate('/publishers'))
      .catch(error => console.error('Error saving publisher:', error));
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Name</label>
        <input type="text" name="name" value={publisher.name} onChange={handleChange} required />
      </div>
      <button type="submit">Save</button>
    </form>
  );
};

export default PublisherForm;
