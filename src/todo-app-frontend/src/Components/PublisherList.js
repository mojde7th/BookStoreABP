import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getPublishers, deletePublisher } from '../services/publisherService';

const PublisherList = () => {
  const [publishers, setPublishers] = useState([]);

  useEffect(() => {
    loadPublishers();
  }, []);

  const loadPublishers = () => {
    getPublishers().then(response => setPublishers(response.data));
  };

  const handleDelete = (id) => {
    deletePublisher(id).then(() => loadPublishers());
  };

  return (
    <div>
      <h1>Publishers</h1>
      <Link to="/publishers/new">Add Publisher</Link>
      <br/>
      <Link to={`/books`}>Back to Book List</Link>
      <ul>
        {publishers.map(publisher => (
          <li key={publisher.id}>
            {publisher.name}
            <Link to={`/publishers/edit/${publisher.id}`}>Edit</Link>
            <button onClick={() => handleDelete(publisher.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default PublisherList;
